import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaTelaFerramentas } from './lista-tela-ferramentas';

describe('ListaTelaFerramentas', () => {
  let component: ListaTelaFerramentas;
  let fixture: ComponentFixture<ListaTelaFerramentas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaTelaFerramentas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaTelaFerramentas);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
