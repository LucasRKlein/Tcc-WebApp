import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { DatePipe } from '@angular/common';
import { environment } from '@environments/environment';
import { Associado } from '@app/models/Associado';
import { AssociadoService } from '@app/services/associado.service';
import { Veiculo } from '@app/models/Veiculo';
import { VeiculoService } from '@app/services/veiculo.service';

@Component({
  selector: 'app-associado-detalhe',
  templateUrl: './associado-detalhe.component.html',
  styleUrls: ['./associado-detalhe.component.scss'],
  providers: [DatePipe],
})
export class AssociadoDetalheComponent implements OnInit {
  modalRef: BsModalRef;
  associadoId: number;
  associado = {} as Associado;
  form: FormGroup;
  veiculoAtual = { id: 0, marcaModelo: '', indice: 0 };
  estadoSalvar = 'post';
  imagemURL = 'assets/img/upload.png';
  file: File;

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  get veiculos(): FormArray {
    return this.form.get('veiculos') as FormArray;
  }

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private associadoService: AssociadoService,
    private veiculoService: VeiculoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private router: Router,
    private datePipe: DatePipe
  ) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarAssociado();
    this.validation();
  }

  public carregarAssociado(): void {
    this.associadoId = +this.activatedRouter.snapshot.paramMap.get('id');

    if (this.associadoId !== null && this.associadoId !== 0) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.associadoService.getAssociadoById(this.associadoId).subscribe((associado: Associado) => {
        this.associado = { ...associado };
        this.form.patchValue(this.associado);
        if (this.associado.imagemUrl !== '') {
          this.imagemURL = environment.apiURL + 'resources/images/' + this.associado.imagemUrl;
        }
        this.carregarVeiculos();
      },
        (error: any) => {
          this.toastr.error('Erro ao tentar carregar Associado.', 'Erro!');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      id: ['', Validators.required],
      nome: ['', Validators.maxLength(100)],
      imagemUrl: [''],
      cpf: [''],
      sexo: [''],
      dataNascimento: [''],
      celular: [''],
      email: ['', Validators.email],
      ruaAvenida: ['', Validators.required],
      numero: ['', Validators.required],
      complemento: ['', Validators.required],
      bairro: ['', [Validators.required]],
      estadoNome: ['', [Validators.required]],
      cidadeNome: ['', [Validators.required]],
      statusCadastro: [''],
      origemCadastro: [''],
      veiculos: this.fb.array([]),
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  cancelarAlteracao() {
    this.router.navigate([`associados`]);
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarAssociado(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.associado = this.estadoSalvar === 'post' ? { ...this.form.value } : { id: this.associado.id, ...this.form.value };
      this.associadoService[this.estadoSalvar](this.associado).subscribe((associadoRetorno: Associado) => {
        this.toastr.success('Associado salvo com Sucesso!', 'Sucesso');
        // this.router.navigate([`associados/detalhe/${associadoRetorno.id}`]);
        this.router.navigate([`associados`]);
      },
        (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toastr.error('Error ao salvar associado', 'Erro');
        },
        () => this.spinner.hide()
      );
    }
  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  uploadImagem(): void {
    this.spinner.show();
    this.associadoService.postUpload(this.associadoId, this.file).subscribe(
      () => {
        this.carregarAssociado();
        this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem', 'Erro!');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }

  public carregarVeiculos(): void {
    this.veiculoService.getVeiculosByAssociadoId(this.associadoId).subscribe((veiculosRetorno: Veiculo[]) => {
      veiculosRetorno.forEach((veiculo) => {
        this.veiculos.push(this.criarVeiculo(veiculo));
      });
    },
      (error: any) => {
        this.toastr.error('Erro ao tentar carregar veiculos', 'Erro');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  adicionarVeiculo(): void {
    this.veiculos.push(this.criarVeiculo({ id: 0 } as Veiculo));
  }

  criarVeiculo(veiculo: Veiculo): FormGroup {
    return this.fb.group({
      id: [veiculo.id],
      placa: [veiculo.placa, Validators.required],
      marcaModelo: [veiculo.marcaModelo, Validators.required],
      anoModelo: [veiculo.anoModelo, Validators.required],
      valorFipe: [veiculo.valorFipe, Validators.required],
    });
  }

  public salvarVeiculos(): void {
    if (this.form.controls.veiculos.valid) {
      this.spinner.show();
      this.veiculoService.saveVeiculo(this.associadoId, this.form.value.veiculos).subscribe(() => {
        this.toastr.success('Veiculos salvos com Sucesso!', 'Sucesso!');
      },
        (error: any) => {
          this.toastr.error('Erro ao tentar salvar veiculos.', 'Erro');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    }
  }

  public removerVeiculo(template: TemplateRef<any>, indice: number): void {
    this.veiculoAtual.id = this.veiculos.get(indice + '.id').value;
    this.veiculoAtual.marcaModelo = this.veiculos.get(indice + '.marcaModelo').value;
    this.veiculoAtual.indice = indice;

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmDeleteVeiculo(): void {
    this.modalRef.hide();
    this.spinner.show();

    this.veiculoService
      .deleteVeiculo(this.associadoId, this.veiculoAtual.id)
      .subscribe(() => {
        this.toastr.success('Veiculo deletado com sucesso', 'Sucesso');
        this.veiculos.removeAt(this.veiculoAtual.indice);
      },
        (error: any) => {
          this.toastr.error(
            `Erro ao tentar deletar o Veiculo ${this.veiculoAtual.id}`,
            'Erro'
          );
          console.error(error);
        }
      ).add(() => this.spinner.hide());
  }

  declineDeleteVeiculo(): void {
    this.modalRef.hide();
  }
}
