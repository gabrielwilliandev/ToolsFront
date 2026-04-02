import { Component, inject, OnInit } from '@angular/core';
import { Button } from '../../components/button/button';
import { MatIconModule } from '@angular/material/icon';
import { Listaesquerda } from '../../components/listaesquerda/listaesquerda';
import { Card } from '../../components/card/card';
import { ListaService } from '../../service/listaService';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Ferramenta } from '../../models/ferramentas';

@Component({
  selector: 'app-minhalista',
  standalone: true,
  imports: [Button, MatIconModule, Listaesquerda, Card],
  templateUrl: './minhalista.html',
  styleUrl: './minhalista.scss',
})
export class Minhalista implements OnInit {
  listas: Ferramenta[] = [];
  listaSelecionada: Ferramenta | null = null;

  constructor(private listaService: ListaService, private router: Router, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.listaService.listar().subscribe({
      next: (data) => {
        this.listas = data;
      },
      error: (err) => {
        const mensagem = err.error?.message || 'Ocorreu um erro ao carregar as listas. Por favor, tente novamente.';
        
        this.snackBar.open(
          mensagem,
          'Fechar',
          { duration: 3000,
            horizontalPosition: 'right',
            verticalPosition: 'top',
            panelClass: ['snackbar-error']
           }
        );
      }
    });
  }

  selecionarLista(lista: Ferramenta) {
    this.listaSelecionada = lista;
  }

  editarLista(lista: Ferramenta) {
    this.router.navigate(['/listaferramentas', lista.id], {
      queryParams: { nome: lista.name }
    });
  }
}
