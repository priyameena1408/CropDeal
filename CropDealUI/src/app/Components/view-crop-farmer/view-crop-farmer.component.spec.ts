import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';

import { ViewCropFarmerComponent } from './view-crop-farmer.component';

describe('ViewCropFarmerComponent', () => {
  let component: ViewCropFarmerComponent;
  let fixture: ComponentFixture<ViewCropFarmerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports:[MatDividerModule,MatIconModule,MatSidenavModule],
      declarations: [ ViewCropFarmerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewCropFarmerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
