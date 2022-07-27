using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace HudElements
{
    public class Healthbar : VisualElement
    {
        public int width { get; set; }
        public int height { get; set; }

        public new class UxmlFactory : UxmlFactory<Healthbar, UxmlTraits> 
        {
            class MyElement : VisualElement
            {
                public new class UxmlFactory : UxmlFactory<MyElement, UxmlTraits> { }

                public new class UxmlTraits : VisualElement.UxmlTraits
                {
                    UxmlIntAttributeDescription m_width = new UxmlIntAttributeDescription() { name = "width", defaultValue = 250 };
                    UxmlIntAttributeDescription m_height = new UxmlIntAttributeDescription() { name = "width", defaultValue = 30 };

                    public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
                    {
                        get { yield break; }
                    }

                    public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
                    {

                    }
                }
            }
        }
    }
}

