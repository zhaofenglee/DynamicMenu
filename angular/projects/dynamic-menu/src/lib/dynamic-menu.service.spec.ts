import { TestBed } from '@angular/core/testing';
import { DynamicMenuService } from './services/dynamic-menu.service';
import { RestService } from '@abp/ng.core';

describe('DynamicMenuService', () => {
  let service: DynamicMenuService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(DynamicMenuService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
