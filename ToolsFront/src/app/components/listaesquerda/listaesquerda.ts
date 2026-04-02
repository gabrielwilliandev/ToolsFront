import { Component, inject, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-listaesquerda',
  imports: [MatIconModule],
  templateUrl: './listaesquerda.html',
  styleUrl: './listaesquerda.scss',
})
export class Listaesquerda {
@Input() id!: number;
@Input() nome!: string;
@Input() data!: string;
@Input() ferramentas!: string[];

  router = inject(Router);

  editarLista(){
  this.router.navigate(['/lista', this.id], {
    queryParams: {nome: this.nome}
  });
}
}
