import { ModuleWithProviders, NgModule } from '@angular/core';
import { DYNAMIC_MENU_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class DynamicMenuConfigModule {
  static forRoot(): ModuleWithProviders<DynamicMenuConfigModule> {
    return {
      ngModule: DynamicMenuConfigModule,
      providers: [DYNAMIC_MENU_ROUTE_PROVIDERS],
    };
  }
}
