using ChemOptimizer;

namespace bnulkTools.Gaussian
{
    internal class GaussianInputPackage2ZhangCang_Tools
    {
        GaussianInputPackage gaussianInputPackage;

        public GaussianInputPackage2ZhangCang_Tools(GaussianInputPackage gaussianInputPackage)
        {
            this.gaussianInputPackage = gaussianInputPackage;
        }

        public MoleculeSpecification Package2MolecularGeometry()
        {
            MoleculeSpecification result = new MoleculeSpecification();
            try
            {
                int cycle = gaussianInputPackage.molecularSpecification.Count;
                for (int i = 0; i < cycle; i++)
                {
                    
                }
            }
            catch
            {
                result = new MoleculeSpecification();
            }


            return result;
        }

    }
}
