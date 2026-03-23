import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ContactService } from '../../../contactService';

@Component({
  selector: 'app-contato',
  imports: [MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, ReactiveFormsModule, CommonModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './contato.html',
  styleUrl: './contato.scss',
})
export class Contato {

  private contactService = inject(ContactService);
  private fb = inject(FormBuilder);

  contatoForm = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    assunto: ['', [Validators.required, Validators.minLength(5)]],
    mensagem: ['', [Validators.required, Validators.minLength(10)]],
    categoria: [0, Validators.required],
  });

  enviarMensagem() {
    if (this.contatoForm.invalid) {
      this.contatoForm.markAllAsTouched();
      return;
    }
      const {email, assunto, mensagem, categoria} = this.contatoForm.getRawValue();

      const payload = {
        userEmail: email,
        subject: assunto,
        body: mensagem,
        category: categoria
      };

      this.contactService.sendContact(payload).subscribe({
        next: () => {
          alert("Mensagem enviada com sucesso!");
          this.contatoForm.reset();
        },
        error: (err) => {
          console.error(err);
          alert("Erro ao enviar mensagem!");
        }
      });
  }
}
