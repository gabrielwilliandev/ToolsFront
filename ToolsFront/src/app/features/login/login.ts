import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from "@angular/router";
import { Button } from '../../components/button/button';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../service/auth-service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  imports: [RouterLink, MatIconModule, Button, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  form: FormGroup;


  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router , private snackBar: MatSnackBar) {
    this.form = fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }
  login(){
    if(this.form.invalid) return;

    this.authService.login(this.form.value.email, this.form.value.password).subscribe({
      next: (response) => {
        console.log(response);
        this.authService.setToken(response.data.token);
        this.router.navigate(['/inicio']);
      },
      error: (err) => {

        const mensagem = err.error?.message || 'Ocorreu um erro durante o login. Por favor, tente novamente.';

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
}
