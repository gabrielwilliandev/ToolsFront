import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Minhalista } from './minhalista';

describe('Minhalista', () => {
  let component: Minhalista;
  let fixture: ComponentFixture<Minhalista>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Minhalista]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Minhalista);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
