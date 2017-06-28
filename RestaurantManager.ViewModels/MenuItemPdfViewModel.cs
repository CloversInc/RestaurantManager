﻿namespace RestaurantManager.ViewModels
{
    using System.Collections.Generic;

    public class MenuItemPdfViewModel
    {
        public MenuItemPdfViewModel()
        {
            MenuItemViewModels = new SortedDictionary<int, ICollection<MenuItemViewModel>>();
        }

        public SortedDictionary<int, ICollection<MenuItemViewModel>> MenuItemViewModels { get; set; }
    }
}
