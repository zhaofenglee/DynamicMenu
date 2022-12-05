import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DynamicMenuComponent } from './components/dynamic-menu.component';
import { DynamicMenuService } from '@j-s.Abp/dynamic-menu';
import { of } from 'rxjs';

describe('DynamicMenuComponent', () => {
  let component: DynamicMenuComponent;
  let fixture: ComponentFixture<DynamicMenuComponent>;
  const mockDynamicMenuService = jasmine.createSpyObj('DynamicMenuService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DynamicMenuComponent],
      providers: [
        {
          provide: DynamicMenuService,
          useValue: mockDynamicMenuService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
