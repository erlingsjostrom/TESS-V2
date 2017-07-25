import { Routes, RouterModule }  from '@angular/router';
import { Pages } from './pages.component';
import { ModuleWithProviders } from '@angular/core';
// noinspection TypeScriptValidateTypes

// export function loadChildren(path) { return System.import(path); };

export const routes: Routes = [
  {
    path: 'login',
    loadChildren: 'app/pages/login/login.module#LoginModule'
  },
  {
    path: 'register',
    loadChildren: 'app/pages/register/register.module#RegisterModule'
  },
  {
    path: '',
    component: Pages,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { 
        path: 'dashboard',
        data: {
          breadcrumb: 'Dashboard',
        },
        loadChildren: './dashboard/dashboard.module#DashboardModule'
      },
      //{ path: 'editors', loadChildren: './editors/editors.module#EditorsModule' },
      //{ path: 'components', loadChildren: './components/components.module#ComponentsModule' },
      //{ path: 'charts', loadChildren: './charts/charts.module#ChartsModule' },
      //{ path: 'ui', loadChildren: './ui/ui.module#UiModule' },
      //{ path: 'forms', loadChildren: './forms/forms.module#FormsModule' },
      //{ path: 'maps', loadChildren: './maps/maps.module#MapsModule' },
      { 
        path: 'offers', 
        data: {
          breadcrumb: 'Offers',
        },
        loadChildren: './offers/offers.module#OffersModule' 
      },
      { 
        path: 'templates', 
        data: {
          breadcrumb: 'Templates',
        },
        loadChildren: './templates/templates.module#TemplatesModule' 
      },
      { 
        path: 'system', 
        data: {
          breadcrumb: 'System',
        },
        loadChildren: './system/system.module#SystemModule' 
      },
    ]
  }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
