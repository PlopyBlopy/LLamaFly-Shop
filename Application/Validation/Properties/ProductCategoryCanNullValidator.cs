﻿using Core.Extensions.Errors;
using Core.Interfaces;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductCategoryCanNullValidator : AbstractValidator<Guid?>
    {
        private ICategoryRepository _categoryRepository;

        public ProductCategoryCanNullValidator(ICategoryRepository repository)
        {
            _categoryRepository = repository;

            RuleFor(categoryId => categoryId)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .MustAsync(async (categoryId, ct) =>
                    IsCategoryExist(categoryId!.Value, ct).Result).WithMessage("Category does not exist");
        }

        private async Task<bool> IsCategoryExist(Guid categoryId, CancellationToken ct)
        {
            var result = await _categoryRepository.IsCategoryExist(categoryId, ct);
            return result.Value;
        }
    }
}