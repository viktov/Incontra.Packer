using Incontra.Packer.Data.Model;
using Incontra.Packer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Incontra.Packer.Api.Models
{
    public class ModelFactory
    {
        public Box Create(InputBox inputBox)
        {
            return new Box(inputBox.w, inputBox.h, inputBox.d, inputBox.wt);
        }

        public InputBox Create(Box box)
        {
            var inputBox = new InputBox
            {
                w = box.Width,
                h = box.Height,
                d = box.Depth,
                x = box.X,
                y = box.Y,
                z = box.Z
            };
            inputBox.wt = box.Weight;
            return inputBox;
        }

        public InputContainer Create(Container container)
        {
            var inputContainer = new InputContainer
            {
                w = container.Width,
                h = container.Height,
                d = container.Depth,
                boxes = new List<InputBox>()
            };
            inputContainer.wt = container.Weight;
            inputContainer.spaces = new List<InputBox>();
            return inputContainer;
        }

        public Container Create(InputContainer inputContainer)
        {
            var container = new Container(inputContainer.w, inputContainer.h, inputContainer.d);
            container.Weight = inputContainer.wt;
            return container;
        }
    }
}