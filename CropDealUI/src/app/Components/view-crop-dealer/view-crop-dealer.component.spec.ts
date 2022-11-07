import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewCropDealerComponent } from './view-crop-dealer.component';

describe('ViewCropDealerComponent', () => {
  let component: ViewCropDealerComponent;
  let fixture: ComponentFixture<ViewCropDealerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewCropDealerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewCropDealerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
