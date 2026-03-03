import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-contato',
  imports: [MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, ReactiveFormsModule, CommonModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './contato.html',
  styleUrl: './contato.scss',
})
export class Contato {

  private fb = inject(FormBuilder);

  contatoForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    assunto: ['', [Validators.required, Validators.minLength(5)]],
    mensagem: ['', [Validators.required, Validators.minLength(10)]],
    categoria: ['', Validators.required],
  });

  enviarMensagem() {
    if (this.contatoForm.invalid) {
      this.contatoForm.markAllAsTouched();
      return;
    }
      const {email, assunto, mensagem, categoria} = this.contatoForm.value;

      console.log('Mensagem enviada:', {email, assunto, mensagem, categoria});
      alert('Sua mensagem foi enviada com sucesso!');
      
      this.contatoForm.reset();
  }
}
