import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Button } from '../../components/button/button';
import { FormsModule } from '@angular/forms';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../service/auth-service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  imports: [MatIcon, Button, FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  form: FormGroup;
  errorMsg = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {
    this.form = this.fb.group(
      {
        name: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        passwordConfirm: ['', [Validators.required]],
      },
      {
        validators: this.passwordMatchValidator,
      },
    );
  }

  passwordMatchValidator(control: AbstractControl) {
    const password = control.get('password')?.value;
    const confirm = control.get('passwordConfirm')?.value;

    if (password !== confirm) {
      return { mismatch: true };
    }

    return null;
  }

  register() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { name, email, password, passwordConfirm } = this.form.value;
    this.authService.register(name, email, password, passwordConfirm).subscribe({
      next: () => {
        this.router.navigate(['']);
      },
      error: (err) => {
        const mensagem =
          err.error?.message || 'Ocorreu um erro durante o registro. Por favor, tente novamente.';
        this.snackBar.open(mensagem, 'Fechar', { duration: 3000 });
      },
    });
  }
}
