import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { take, switchMap } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  return authService.token.pipe(
    take(1),
    switchMap((authToken) => {
      const authReq = authToken
        ? req.clone({
            setHeaders: {
              Authorization: `Bearer ${authToken}`,
            },
          })
        : req;

      return next(authReq);
    })
  );
};
