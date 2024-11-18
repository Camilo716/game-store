import { APP_INITIALIZER, ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ConfigService } from './core/services/config.service';
import { HttpMessageInterceptor } from './core/interceptors/HttpMessageInterceptor';
import { AuthInterceptor } from './core/interceptors/AuthInterceptor';

export function initializeApp(
  configService: ConfigService
): () => Promise<void> {
  return async () => await configService.loadConfig();
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(
      withInterceptors([HttpMessageInterceptor, AuthInterceptor])
    ),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [ConfigService],
      multi: true,
    },
  ],
};
