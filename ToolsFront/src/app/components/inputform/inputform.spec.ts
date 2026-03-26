import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Inputform } from './inputform';

describe('Inputform', () => {
  let component: Inputform;
  let fixture: ComponentFixture<Inputform>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Inputform]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Inputform);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
