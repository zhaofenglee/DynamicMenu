import {makeEnvironmentProviders} from '@angular/core';
import { DYNAMIC_MENU_ROUTE_PROVIDERS } from './providers/route.provider';

export function provideDynamicMenuConfig() {
  return makeEnvironmentProviders([DYNAMIC_MENU_ROUTE_PROVIDERS])
}
