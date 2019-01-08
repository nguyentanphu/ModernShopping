using System;
using System.Collections.Generic;
using System.Text;
using ModernShopping.Application.Exceptions;

namespace ModernShopping.Application.Utils
{
    public static class HelperCore
    {
        public static void CheckSaveChange(int affectedRow, int expectedRow)
        {
            if (affectedRow < expectedRow)
                throw new EntityMissingSaveException($"Expected {expectedRow} row affected, but rowAffected={affectedRow}");
        }
    }
}
