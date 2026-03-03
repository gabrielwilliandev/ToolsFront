import { FormsModule } from '@angular/forms';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatIconModule } from  '@angular/material/icon' ;

@Component({
  selector: 'app-tag',
  standalone: true,
  imports: [MatIconModule, FormsModule],
  templateUrl: './tag.html',
  styleUrl: './tag.scss',
})
export class Tag {
  @Input() tag!: string;

  @Output() editar = new EventEmitter<string>();

  editando = false;
  valorEditado = '';

  iniciarEdicao() {
    this.editando = true;
    this.valorEditado = this.tag;
  }
  salvarEdicao() {
    if (this.valorEditado.trim()) {
    this.editar.emit(this.valorEditado);
  }
  this.editando = false;
  }

  cancelarEdicao() {
    this.editando = false;
  }
}
