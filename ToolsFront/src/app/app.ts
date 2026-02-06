import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { Ferramenta, ListaService } from './listaService';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {

  service = inject(ListaService)
  toastrService = inject(ToastrService)

  

  public botaoVisibilidade: boolean = false;
  public tagDigitada: string= '';
  public nome: string = '';
  public descricao: string ='';

  public listaTags: string[] = [];
  
 
  constructor() {
    
    
  }

   ngOnInit(): void {
    
    this.service.carregar();
    this.botaoVisibilidade = this.service.getLista().length > 0;
  }

  adicionarFerramenta(){
    if(this.nome.trim() === ''){
    this.toastrService.warning('Nome inválido!');
    return;
  }
  this.service.adicionar({
    nome: this.nome,
    descricao: this.descricao,
    tags: [...this.listaTags]
  });

  this.limparInputs();

  this.botaoVisibilidade = true;
}
  
    deletarItem(item: number){

      this.service.removerItem(item);

      this.botaoVisibilidade = this.service.getLista().length > 0;
    
  }

    excluirLista(){
      this.service.limparLista();

      this.botaoVisibilidade = this.botaoVisibilidade = this.service.getLista().length > 0;
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

    limparInputs(){
      this.tagDigitada = '';
      this.nome = '';
      this.descricao = '';
      this.listaTags = [];
    }

}
