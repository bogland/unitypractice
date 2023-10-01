using oh;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace oh
{
    class PageSampleProps : UIProperty
    {
        public string title;
        public string description;
    }

    public class PageSample : BaseUI
    {
        public TMP_Text textTitle;
        public TMP_Text textDescription;

        PageSampleProps props;

        public override void Init(UIProperty property)
        {
            props = (PageSampleProps)Convert.ChangeType(property, typeof(PageSampleProps));
            textTitle.text = props.title;
            textDescription.text = props.description;
        }

    }
}
