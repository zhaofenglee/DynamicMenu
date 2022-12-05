import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class DynamicMenuService {
  apiName = 'DynamicMenu';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/DynamicMenu/sample' },
      { apiName: this.apiName }
    );
  }
}
