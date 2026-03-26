import { Component } from '@angular/core';
import { Button } from '../../components/button/button';
import { MatIconModule } from '@angular/material/icon';
import { Listaesquerda } from '../../components/listaesquerda/listaesquerda';

@Component({
  selector: 'app-minhalista',
  imports: [Button, MatIconModule, Listaesquerda],
  templateUrl: './minhalista.html',
  styleUrl: './minhalista.scss',
})
export class Minhalista {

}
