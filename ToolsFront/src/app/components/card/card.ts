import { Component, inject } from '@angular/core';
import { ListaService } from '../../listaService';
import { Tag } from '../tag/tag';
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-card',
  imports: [Tag, MatIconModule],
  templateUrl: './card.html',
  styleUrl: './card.scss',
})
export class Card {
  service = inject(ListaService);



  deletarItem(item: number) {
    this.service.removerItem(item);
  }

  excluirLista() {
    this.service.limparLista();
  }

  atualizarTag(itemIndex: number, tagIndex: number, novaTag: string) {
    const lista = this.service.getLista();
    if (!lista[itemIndex]) return;

    const novasTags = [...lista[itemIndex].tags];
    novasTags[tagIndex] = novaTag;
    this.service.atualizaTags(itemIndex, novasTags);
  }
}
