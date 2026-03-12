import { Component, inject } from '@angular/core';
import { ListaService } from '../../listaService';
import { Tag } from '../tag/tag';
import { MatIconModule } from '@angular/material/icon';
import { Ferramenta } from '../../models/ferramentas';
@Component({
  selector: 'app-card',
  imports: [Tag, MatIconModule],
  templateUrl: './card.html',
  styleUrl: './card.scss',
})
export class Card {
  service = inject(ListaService);

  ferramentas: Ferramenta[] = [];

  ngOnInit(){
    this.service.carregarCache();
    this.ferramentas = this.service.getCache();
  }

  deletarItem(id: string) {
    this.service.removerItem(id).subscribe(() => {
      this.ferramentas = this.service.getCache();
    })
  }

  atualizarTag(itemIndex: number, tagIndex: number, novaTag: string) {
    const lista = this.ferramentas[itemIndex]
    if (!lista) return;

    const novasTags = [...lista.tags];
    novasTags[tagIndex] = novaTag;

    this.service.atualizar(lista.id, {
      title: lista.title,
      description: lista.description,
      tags: novasTags
    }).subscribe();
  }
}
