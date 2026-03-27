import { Component, inject } from '@angular/core';
import { Button } from '../../components/button/button';
import { MatIconModule } from '@angular/material/icon';
import { Listaesquerda } from '../../components/listaesquerda/listaesquerda';
import { Card } from '../../components/card/card';

@Component({
  selector: 'app-minhalista',
  standalone: true,
  imports: [Button, MatIconModule, Listaesquerda, Card],
  templateUrl: './minhalista.html',
  styleUrl: './minhalista.scss',
})
export class Minhalista {


  listaSelecionada: any = null;

  listas = [
  {
    id: 1,
    nome: 'Estudos Angular',
    data: '27 de março de 2026',
    ferramentas: 3
  },
  {
    id: 2,
    nome: 'Projeto Dashboard',
    data: '26 de março de 2026',
    ferramentas: 5
  },
  {
    id: 3,
    nome: 'Back-end C#',
    data: '25 de março de 2026',
    ferramentas: 2
  }
];

selecionarLista(lista: any){
  this.listaSelecionada = lista;
}


}
