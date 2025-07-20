import { Component, inject } from '@angular/core';
import { CoreService } from '../../services/core.service';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MaterialModule } from '../../material.module';
import { NgIf } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  imports: [
    RouterModule,
    MaterialModule,
    NgIf,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
private settings = inject(CoreService);
  private router = inject(Router);
  private authService = inject(AuthService);

  options = this.settings.getOptions();

  form = new FormGroup({
    uname: new FormControl('', [Validators.required, Validators.minLength(6)]),
    password: new FormControl('', [Validators.required]),
  });

  get f() {
    return this.form.controls;
  }

  submit() {
    if (this.form.valid) {
      const credentials = {
        username: this.f.uname.value!,
        password: this.f.password.value!,
        appName: 'ProdMon',
      };

      this.authService.login(credentials).subscribe({
        next: (response) => {
          // Store tokens in localStorage
          this.authService.setTokens(
            response.accessToken,
            response.refreshToken
          );

          // Navigate to the desired route
          this.router.navigate(['/starter']);
        },
        error: (error) => {
          console.error('Login failed: ', error);
        },
      });
    }
  }
}
