using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputUtilities
{
    public static class InputManager
    {
        static InputActions s_InputActions;

        public static InputActions InputActions
        {
            get
            {
                if (s_InputActions == null)
                {
                    s_InputActions = new InputActions();
                }

                return s_InputActions;
            }
        }
    }
}
