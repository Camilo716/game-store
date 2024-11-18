import { HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, tap, throwError } from 'rxjs';

export const HttpMessageInterceptor: HttpInterceptorFn = (req, next) => {
  const snackBar = inject(MatSnackBar);

  const excludedEndpoints = ['/api/users/login'];

  const showMessage = (message: string) =>
    snackBar.open(message, 'Close', {
      duration: 5000,
      horizontalPosition: 'right',
      verticalPosition: 'bottom',
      panelClass: [],
    });

  return next(req).pipe(
    tap((event: any) => {
      if (event instanceof HttpResponse) {
        const shouldExclude = excludedEndpoints.some((endpoint) =>
          req.url.includes(endpoint)
        );

        if (!shouldExclude && req.method !== 'GET') {
          showMessage('Action completed successfully!');
        }
      }
    }),
    catchError((error) => {
      const shouldExclude = excludedEndpoints.some((endpoint) =>
        req.url.includes(endpoint)
      );

      if (shouldExclude) {
        return throwError(() => error);
      }

      const authError = error.status === 401 && error.status < 403;
      if (authError) {
        showMessage("You don't have permissions to perform this action.");
      }

      const serverError = error.status >= 500 && error.status < 600;
      if (serverError) {
        showMessage('An error occurred. Please try again.');
      }

      return throwError(() => error);
    })
  );
};
