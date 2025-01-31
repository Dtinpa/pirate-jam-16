using System;

namespace Code.Runtime {
    /// <summary>
    /// Apply to any <see cref="System.Reflection.FieldInfo">field</see> or <see cref="System.Reflection.PropertyInfo">property</see> to have
    /// it rendered into the inspector. It's not perfect, but it gets the job done.
    /// </summary>
    /// <remarks>
    /// Actual rendering is done by <see cref="T:Code.Editor.MonoBehaviourEditor2"/>.
    ///
    /// TODO: move into <see cref="BrandonUtils"/>
    /// </remarks>
    public class ShowInEditorAttribute : Attribute { }
}