import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllStaffsComponent } from './all-staffs.component';

describe('AllStaffsComponent', () => {
  let component: AllStaffsComponent;
  let fixture: ComponentFixture<AllStaffsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllStaffsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AllStaffsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
