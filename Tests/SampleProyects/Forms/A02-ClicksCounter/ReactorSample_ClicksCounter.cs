using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class ReactorSample_ClicksCounter : MonoBehaviour
{

    private REcanvas reactorList1;


    void Start()
    {

        // Multiple components
        reactorList1 = ListREcomponent().Draw();

        var find = REtext.Find("#Title-One");
        Debug.Log("Elements Finded: " + find.Length);

        foreach (var element in find)
        {
            Debug.Log("Type: " + element.textCmp.text);
        }

    }

    private REcanvas ListREcomponent()
    {
        return new REcanvas
        {
            childs = () => new REbase[]
            {
                new REtext
                {
                    propsId = () => new REtext.IdSetter
                    {
                        id = "Title-One",
                        className = new string[]
                        {
                            "H1",
                            "Text",
                            "Title",
                        },
                    },
                },
                new REtext
                {
                    propsId = () => new REtext.IdSetter
                    {
                        id = "Title-One",
                        className = new string[]
                        {
                            "H1",
                            "Text",
                            "Title",
                        },
                    },
                },
                new REtext
                {
                },
                new REimage
                {
                },
            }
        };
    }

}

