import { Component } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-inicio',
  imports: [MatIconModule, FormsModule],
  templateUrl: './inicio.html',
  styleUrl: './inicio.scss',
})
export class Inicio {
  nomeLista = ''

  
  constructor(private router: Router) {}

  criarLista(){
    if(!this.nomeLista.trim()) return;
      this.router.navigate(['/listaferramentas'],{
        queryParams: {nome: this.nomeLista}
      })
  }
}
