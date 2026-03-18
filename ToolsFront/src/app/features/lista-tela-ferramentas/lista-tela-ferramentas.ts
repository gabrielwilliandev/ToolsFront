import { Component, inject, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ListaService } from '../../listaService';
import { FormsModule } from '@angular/forms';
import { Tag } from '../../components/tag/tag';
import { MatIconModule } from '@angular/material/icon';
import { Card } from '../../components/card/card';

@Component({
  selector: 'app-lista-tela-ferramentas',
  imports: [FormsModule, Tag, MatIconModule, Card],
  templateUrl: './lista-tela-ferramentas.html',
  styleUrl: './lista-tela-ferramentas.scss',
})
export class ListaTelaFerramentas implements OnInit {
  service = inject(ListaService);
  toastrService = inject(ToastrService);

  public botaoVisibilidade: boolean = false;
  public tagDigitada: string = '';
  public nome: string = '';
  public descricao: string = '';

  public listaTags: string[] = [];

  constructor() {}

  ngOnInit(): void {
    this.service.carregarCache();

    this.service.listar().subscribe({
      next: (data) => {
        this.botaoVisibilidade = data.length> 0;
      },
      error: (err) => {
        console.error(err);
        this.toastrService.error('Erro ao carregar ferramentas!')
      }
    })
  }

  adicionarFerramenta() {
    if (this.nome.trim() === '') {
      this.toastrService.warning('Nome inválido!');
      return;
    }
    this.service.adicionar({
      name: this.nome,
      description: this.descricao,
      tags: [...this.listaTags],
    }).subscribe({
      next: () => {
        this.toastrService.success('Ferramenta adicionada!');
        this.limparInputs();
        this.botaoVisibilidade = true;
        this.service.listar().subscribe();
      },
      error: (err) => {
        console.error(err);
        this.toastrService.error('Erro ao adicionar ferramenta!')
      }
    })

    this.botaoVisibilidade = true;
  }

  processarTags(event: KeyboardEvent) {
    if (event.key === 'Enter' || event.key === ',') {
      const tag = this.tagDigitada.replace(',', '').trim();

      if (tag && !this.listaTags.includes(tag)) {
        this.listaTags.push(tag);
      }

      this.tagDigitada = '';
    }
  }

  atualizarTag(tagIndex: number, novaTag: string) {
    this.listaTags[tagIndex] = novaTag;
    this.listaTags = [...this.listaTags];
  }

  limparInputs() {
    this.tagDigitada = '';
    this.nome = '';
    this.descricao = '';
    this.listaTags = [];
  }
}
