import { Component, inject } from '@angular/core';
import { ListaService } from '../../listaService';
import { Tag } from '../tag/tag';
import { MatIconModule } from '@angular/material/icon';
import { Ferramenta } from '../../models/ferramentas';
import { AsyncPipe } from '@angular/common';
@Component({
  selector: 'app-card',
  imports: [Tag, MatIconModule, AsyncPipe],
  standalone: true,
  templateUrl: './card.html',
  styleUrl: './card.scss',
})
export class Card {
  service = inject(ListaService);
  
  ferramentas$ = this.service.ferramentas$;

  ngOnInit(){
    this.service.listar().subscribe();
  }

  deletarItem(id: string) {
    this.service.removerItem(id).subscribe();
  }

  atualizarTag(item: Ferramenta, tagIndex: number, novaTag: string) {

    const novasTags = [...item.tags];
    novasTags[tagIndex] = novaTag;

    this.service.atualizar(item.id, {
      name: item.name,
      description: item.description,
      tags: novasTags
    }).subscribe();
  }
}
