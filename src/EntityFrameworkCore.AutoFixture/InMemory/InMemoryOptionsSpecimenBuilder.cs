﻿using System;
using AutoFixture.Kernel;
using EntityFrameworkCore.AutoFixture.Core;

namespace EntityFrameworkCore.AutoFixture.InMemory
{
    public class InMemoryOptionsSpecimenBuilder : ISpecimenBuilder
    {
        public IRequestSpecification OptionsSpecification { get; }

        public InMemoryOptionsSpecimenBuilder(IRequestSpecification optionsSpecification)
        {
            this.OptionsSpecification = optionsSpecification
                ?? throw new ArgumentNullException(nameof(optionsSpecification));
        }

        public InMemoryOptionsSpecimenBuilder()
            : this(new IsOptionsBuilder())
        {
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!this.OptionsSpecification.IsSatisfiedBy(request))
            {
                return new NoSpecimen();
            }

            return new InMemoryOptionsBuilder();
        }

        private class IsOptionsBuilder : IRequestSpecification
        {
            public bool IsSatisfiedBy(object request)
            {
                return request is Type type
                       && type.IsInterface
                       && type == typeof(IOptionsBuilder);
            }
        }
    }
}
