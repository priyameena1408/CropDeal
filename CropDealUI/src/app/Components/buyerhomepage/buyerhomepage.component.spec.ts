import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuyerhomepageComponent } from './buyerhomepage.component';

describe('BuyerhomepageComponent', () => {
  let component: BuyerhomepageComponent;
  let fixture: ComponentFixture<BuyerhomepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BuyerhomepageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BuyerhomepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
