import { CommentType } from './../core/models/game-comment';
import { Component, inject, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { CommentTreeComponent } from './comment-tree/comment-tree.component';
import { GameFormData } from '../admin/game/game-form/gameFormData';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { Game } from '../core/models/game';
import { CommentService } from '../core/services/comment.service';
import { GameComment } from '../core/models/game-comment';
import { CommentFormComponent } from './comment-form/comment-form.component';

@Component({
  selector: 'app-game-comment',
  standalone: true,
  imports: [MatCardModule, MatIconModule, CommentTreeComponent],
  templateUrl: './game-comment.component.html',
  styleUrl: './game-comment.component.css',
})
export class GameCommentComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<GameCommentComponent>);
  readonly gameFormData = inject<GameFormData>(MAT_DIALOG_DATA);

  readonly formDialog = inject(MatDialog);

  title = 'Comments';

  comments: GameComment[] = [];

  game: Game;

  constructor(private commentService: CommentService) {
    this.game = this.gameFormData.game;
  }
  ngOnInit(): void {
    this.loadComments();
  }

  loadComments() {
    this.commentService
      .getByGameKey(this.game.key)
      .subscribe((comments: GameComment[]) => {
        this.comments = comments;
      });
  }

  openAddCommentDialog() {
    const dialogRef = this.formDialog.open(CommentFormComponent, {
      data: {
        comment: {
          type: CommentType.Comment,
        },
        gameKey: this.game.key,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadComments();
    });
  }
}
