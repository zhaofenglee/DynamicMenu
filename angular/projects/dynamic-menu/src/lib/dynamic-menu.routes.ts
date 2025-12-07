import { RouterOutletComponent } from '@abp/ng.core';
import { Routes } from '@angular/router';
import { DynamicMenuComponent } from './components/dynamic-menu.component';

export const dynamicMenuRoutes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: RouterOutletComponent,
    children: [
      {
        path: '',
        component: DynamicMenuComponent,
      },
    ],
  },
];
