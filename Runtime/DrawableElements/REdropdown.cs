﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REdropdown : ReactorChildElement
    {
        protected override string elementType => "Dropdown";

        protected override Func<RectTransformSetter> PropsRectTransform => throw new NotImplementedException();

        protected override void AddComponents()
        {
            throw new NotImplementedException();
        }

        protected override void AddHooks()
        {
            throw new NotImplementedException();
        }

        protected override ElementSelector AddSelector()
        {
            throw new NotImplementedException();
        }
    }
}
