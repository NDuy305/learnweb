import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatCard } from '@angular/material/card';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sever-error',
  imports: [
    MatCard
  ],
  templateUrl: './sever-error.component.html',
  styleUrl: './sever-error.component.scss'
})
export class SeverErrorComponent {
  error?: any;  

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras.state?.['error'];
  }
}
