import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Detalhedireita } from './detalhedireita';

describe('Detalhedireita', () => {
  let component: Detalhedireita;
  let fixture: ComponentFixture<Detalhedireita>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Detalhedireita]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Detalhedireita);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
