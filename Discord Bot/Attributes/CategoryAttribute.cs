﻿using System;
namespace Discord_Bot.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
     public sealed class CategoryAttribute : Attribute
    {
        public readonly string Category;

        public CategoryAttribute(string category)
        {
            this.Category = category;
        }
        
    }
}
