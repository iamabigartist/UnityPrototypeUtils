using System;
using UnityEngine.UIElements;
using static PrototypePackages.PrototypeUtils.Runtime.UIUtils;
namespace PrototypePackages.PrototypeUtils.Runtime
{
    /// <summary>
    ///     <para>Provide the <see cref="VisualElement" /> management and ease syntax</para>
    ///     <remarks>
    ///         <para>Apply means from UI to module</para>
    ///         <para>Update means from module to UI</para>
    ///     </remarks>
    /// </summary>
    public class UIElementManager
    {
        event Action OnUpdate;
        public VisualElement root;

        public UIElementManager(VisualElement root)
        {
            this.root = root;
        }

        /// <summary>
        ///     Need to be called when need to update the UI view content from the module.
        /// </summary>
        public void Update() { OnUpdate?.Invoke(); }

        public T Add<T>(T visualElement)
            where T : VisualElement
        {
            root.Add( visualElement );

            return visualElement;
        }

        public TVisualElement ApplyTo<TVisualElement, TValue>(
            TVisualElement visualElement,
            EventCallback<ChangeEvent<TValue>> apply)
            where TVisualElement : VisualElement, INotifyValueChanged<TValue>
        {
            root.Add( visualElement );
            visualElement.RegisterValueChangedCallback( apply );

            return visualElement;
        }

        public TFieldVisualElement UpdateFrom<TFieldVisualElement, TValue>(
            TFieldVisualElement field,
            Func<TValue> data_getter)
            where TFieldVisualElement : BaseField<TValue>
        {
            OnUpdate += () =>
            {
                UpdateValue(
                    () => field.value,
                    data_getter,
                    value => field.value = value );
            };

            return field;
        }

        public TTextVisualElement UpdateFrom<TTextVisualElement>(
            TTextVisualElement text,
            Func<string> str_getter)
            where TTextVisualElement : TextElement
        {
            OnUpdate += () =>
            {
                UpdateValue(
                    () => text.text,
                    str_getter,
                    value => text.text = value );
            };

            return text;
        }

    }
}
