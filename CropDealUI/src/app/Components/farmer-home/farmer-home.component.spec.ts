import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmerHomeComponent } from './farmer-home.component';

describe('FarmerHomeComponent', () => {
  let component: FarmerHomeComponent;
  let fixture: ComponentFixture<FarmerHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmerHomeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FarmerHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
