import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App {
  constructor(private router: Router) {}

  navigate(path: string) {
    this.router.navigate([path]);
  }

  navigateHome() {
  this.router.navigate(['/']);
}
}