namespace Alexandria.Client.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    public class Presenters
    {
        public static void Show(string name, params object[] args)
        {
            var instance = CreateInstance(name, args);

            instance.Show();
        }

        public static T ShowDialog<T>(string name, params object[] args)
        {
            var instance = CreateInstance(name, args);

            instance.ShowDialog();

            return (T) instance.Result;
        }

        private static IPresenter CreateInstance(string name, object[] args)
        {
            var type = Assembly.GetExecutingAssembly().GetType("Effectus.Features." + name + ".Presenter");
            if (type == null)
                throw new InvalidOperationException("Could not find presenter: " + name);

            var instance = (IPresenter) Activator.CreateInstance(type);

            WireEvents(instance);
            WireButtons(instance);
            WireListBoxesDoubleClick(instance);

            if (args != null && args.Length > 0)
            {
                var init = type.GetMethod("Initialize");
                if (init == null)
                    throw new InvalidOperationException(
                        "Presnter to be shown we argument, but not initialize method found");

                init.Invoke(instance, args);
            }
            return instance;
        }

        private static void WireListBoxesDoubleClick(IPresenter presenter)
        {
            var presenterType = presenter.GetType();
            var methodsAndListBoxes = from method in GetActionMethods(presenterType)
                                      where method.Name.EndsWith("Choosen")
                                      where method.GetParameters().Length == 1
                                      let elementName =
                                          method.Name.Substring(2, method.Name.Length - 2 /*On*/- 7 /*Choosen*/)
                                      let matchingListBox =
                                          LogicalTreeHelper.FindLogicalNode(presenter.View, elementName) as ListBox
                                      where matchingListBox != null
                                      select new {method, matchingListBox};

            foreach (var methodAndEvent in methodsAndListBoxes)
            {
                var parameterType = methodAndEvent.method.GetParameters()[0].ParameterType;
                var action = Delegate.CreateDelegate(typeof (Action<>).MakeGenericType(parameterType),
                                                     presenter, methodAndEvent.method);

                methodAndEvent.matchingListBox.MouseDoubleClick += (sender, args) =>
                                                                       {
                                                                           var item1 = ((ListBox) sender).SelectedItem;
                                                                           if (item1 == null)
                                                                               return;
                                                                           action.DynamicInvoke(item1);
                                                                       };
            }
        }

        private static void WireButtons(IPresenter presenter)
        {
            var presenterType = presenter.GetType();
            var methodsAndButtons =
                from method in GetParameterlessActionMethods(presenterType)
                let elementName = method.Name.Substring(2)
                let matchingControl = LogicalTreeHelper.FindLogicalNode(presenter.View, elementName) as Button
                let fact = presenterType.GetProperty("Can" + elementName)
                where matchingControl != null
                select new {method, fact, button = matchingControl};

            foreach (var matching in methodsAndButtons)
            {
                var action = (Action) Delegate.CreateDelegate(typeof (Action),
                                                              presenter, matching.method);
                Fact fact = null;
                if (matching.fact != null)
                    fact = (Fact) matching.fact.GetValue(presenter, null);
                matching.button.Command = new DelegatingCommand(action, fact);
            }
        }

        /// <summary>
        /// Here we simply match methods on the presenter to events on the view
        /// The convention is that any method started with "On" and having no parameters
        /// will be matched with an event with the same name (without the On prefix)
        /// assuming that the event is of RoutedEventHandler type.
        /// </summary>
        private static void WireEvents(IPresenter presenter)
        {
            var viewType = presenter.View.GetType();
            var presenterType = presenter.GetType();
            var methodsAndEvents =
                from method in GetParameterlessActionMethods(presenterType)
                let matchingEvent = viewType.GetEvent(method.Name.Substring(2))
                where matchingEvent != null
                where matchingEvent.EventHandlerType == typeof (RoutedEventHandler)
                select new {method, matchingEvent};

            foreach (var methodAndEvent in methodsAndEvents)
            {
                var action = (Action) Delegate.CreateDelegate(typeof (Action),
                                                              presenter, methodAndEvent.method);

                var handler = (RoutedEventHandler) ((sender, args) => action());
                methodAndEvent.matchingEvent.AddEventHandler(presenter.View, handler);
            }
        }

        private static IEnumerable<MethodInfo> GetActionMethods(Type type)
        {
            return
                from method in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                where method.Name.StartsWith("On")
                select method;
        }

        private static IEnumerable<MethodInfo> GetParameterlessActionMethods(Type type)
        {
            return from method in GetActionMethods(type)
                   where method.GetParameters().Length == 0
                   select method;
        }
    }
}