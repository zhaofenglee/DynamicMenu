import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DynamicMenuComponent } from './components/dynamic-menu.component';
import { DynamicMenuRoutingModule } from './dynamic-menu-routing.module';

@NgModule({
  declarations: [DynamicMenuComponent],
  imports: [CoreModule, ThemeSharedModule, DynamicMenuRoutingModule],
  exports: [DynamicMenuComponent],
})
export class DynamicMenuModule {
  static forChild(): ModuleWithProviders<DynamicMenuModule> {
    return {
      ngModule: DynamicMenuModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<DynamicMenuModule> {
    return new LazyModuleFactory(DynamicMenuModule.forChild());
  }
}
