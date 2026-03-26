import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Listaesquerda } from './listaesquerda';

describe('Listaesquerda', () => {
  let component: Listaesquerda;
  let fixture: ComponentFixture<Listaesquerda>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Listaesquerda]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Listaesquerda);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
