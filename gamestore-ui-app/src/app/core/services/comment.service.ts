import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs';
import { GameComment } from '../models/game-comment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  constructor(
    private http: HttpClient,
    private config: ConfigService,
    private auth: AuthService
  ) {}

  getByGameKey(gameKey: string): Observable<any> {
    return this.http.get(this.config.getCommentsByGameKeyApiUrl(gameKey));
  }

  addComment(comment: GameComment, gameKey: string): Observable<any> {
    comment.userName = this.auth.getUsername() || '';
    let request = {
      comment: comment,
      parentId: comment.parentCommentId,
    };

    return this.http.post(this.config.addCommentApiUrl(gameKey), request);
  }
}
